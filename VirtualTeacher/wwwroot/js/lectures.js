$(document).ready(function () {
    $(document).on('submit', '#assignmentForm', function (event) {
        // Prevent the default form submission behavior
        event.preventDefault();

        // Serialize the form data
        var formData = new FormData($(this)[0]);

        var assignmentId = $(this).find('input[name="assignmentId"]').val();

        formData.append('assignmentId', assignmentId);

        var _url = $(this).attr('action');
        // Submit the form data asynchronously
        $.ajax({
            method: "POST",
            url: $(this).attr('action'), // Use the form's action URL
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    // Display the success message
                    $('#assignmentMessage').html('<div class="alert alert-success" role="alert">Successfully submitted assignment</div>');

                    // Disable the upload button
                    $('#uploadButton').prop('disabled', true);
                    $('#file').prop('disabled', true);
                } else {
                    // Display an error message
                    $('#assignmentMessage').html('<div class="alert alert-danger" role="alert">Failed to submit assignment. Assignment already submitted.</div>');
                }
            },
            error: function (xhr, status, error) {
                // Handle errors if needed
                console.error(xhr.responseText);
            }
        });
    });
    // Event handler for clicking lecture links
    // Event handler for clicking lecture links
    $(document).on('click', '.list-group-item-action', async function () {
        var videoUrl = $(this).data('video-url');
        var assignmentContents = $(this).data('assignment-contents');
        var assignmentId = $(this).data('assignment-id');
        $('#lectureContent').html('');

        if ($(this).hasClass('video-link') && videoUrl) {
            var videoId = videoUrl.split('v=')[1];
            var ampersandPosition = videoId.indexOf('&');
            if (ampersandPosition !== -1) {
                videoId = videoId.substring(0, ampersandPosition);
            }
            var embedUrl = "https://www.youtube.com/embed/" + videoId;
            $('#lectureContent').append('<iframe id="lectureVideo" style="width:100%; height:80vh" src="' + embedUrl + '" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>');
        } else if ($(this).hasClass('assignment-link') && assignmentContents) {
            $('#lectureContent').append('<h4>Assignment</h4>');
            for (var i = 0; i < assignmentContents.length; i++) {
                var content = assignmentContents[i];

                var typeMap = {
                    0: 'URL',
                    1: 'File',
                    2: 'PlainText'
                };

                var contentType = typeMap[content.Type];

                switch (contentType) {
                    case 'File':
                        await fetchPdfContent(assignmentId, content.Content);
                        break;
                    case 'PlainText':
                        $('#lectureContent').append('<p>' + String(content.Content) + '</p>');
                        break;
                    case 'URL':
                        $('#lectureContent').append('<p><a href="' + content.value + '" target="_blank">' + content.Content + '</a></p>');
                        break;
                    default:
                        // Handle unknown content type
                        break;
                }

            }
            appendAssignmentForm(assignmentId)
        } else {
            $('#lectureContent').html('<p>No content available for this selection.</p>');
        }
    });

    function fetchPdfContent(assignmentId, folderPath) {
        return new Promise((resolve, reject) => {
            var xhr = new XMLHttpRequest();
            xhr.open('GET', '/Assignment/GetContentFile?folderPath=' + encodeURIComponent(folderPath), true); // Pass folderPath as a query parameter
            xhr.responseType = 'arraybuffer';

            xhr.onload = function () {
                if (this.status === 200) {
                    var blob = new Blob([this.response], { type: 'application/pdf' });
                    var fileObjectUrl = URL.createObjectURL(blob);
                    $('#lectureContent').append('<iframe src="' + fileObjectUrl + '" style="width:100%; height:600px;" frameborder="0"></iframe>');

                    // Resolve the promise once the PDF viewer is loaded
                    resolve();
                } else {
                    console.error("Failed to fetch PDF content.");
                    $('#lectureContent').html('<p>Error: Unable to fetch file content</p>');
                    reject();
                }
            };

            xhr.onerror = function () {
                console.error("Failed to fetch PDF content.");
                $('#lectureContent').html('<p>Error: Unable to fetch file content</p>');
                reject();
            };

            xhr.send();
        });
    }
    function appendAssignmentForm(assignmentId) {
        $('#lectureContent').append('<form id="assignmentForm" style="font-family: \'Poppins\', sans-serif;" method="post" enctype="multipart/form-data" action="/Assignment/Upload">' +
            '<input type="hidden" name="assignmentId" value="' + assignmentId + '">' + // Add assignmentId as a hidden input field
            '<div style="max-width:30vh; margin-bottom:10px" class="form-group">' +
            '<label style="cursor: unset">Submit assignment:</label>' +
            '<input style="font-size:15px" type="file" name="file" id="file" class="form-control" required />' +
            '</div>' +
            '<div id="assignmentMessage"></div>' + // Container for success message
            '<button id="uploadButton" type="submit" class="btn btn-primary">Upload</button>' +
            '</form>');
    }
});
