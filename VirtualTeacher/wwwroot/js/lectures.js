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
    $(document).on('click', '.list-group-item-action', function () {
        var videoUrl = $(this).data('video-url');
        var assignmentContent = $(this).data('assignment-content');
        $('#lectureContent').html('');

        if ($(this).hasClass('video-link') && videoUrl) {
            var videoId = videoUrl.split('v=')[1];
            var ampersandPosition = videoId.indexOf('&');
            if (ampersandPosition !== -1) {
                videoId = videoId.substring(0, ampersandPosition);
            }
            var embedUrl = "https://www.youtube.com/embed/" + videoId;
            $('#lectureContent').append('<iframe id="lectureVideo" style="width:100%; height:80vh" src="' + embedUrl + '" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>');
        } else if ($(this).hasClass('assignment-link') && assignmentContent) {
            $('#lectureContent').append('<h4>Assignment</h4>');
            $('#lectureContent').append('<p>' + assignmentContent + '</p>');
            var assignmentId = $(this).data('assignment-id');
            $('#lectureContent').append('<form id="assignmentForm" style="font-family: \'Poppins\', sans-serif;" method="post" enctype="multipart/form-data" action="/Assignment/Upload">' +
                '<input type="hidden" name="assignmentId" value="' + assignmentId + '">' + // Add assignmentId as a hidden input field
                '<div style="max-width:30vh; margin-bottom:10px" class="form-group">' +
                '<label style="cursor: unset">Submit assignment:</label>' +
                '<input style="font-size:15px" type="file" name="file" id="file" class="form-control" required />' +
                '</div>' +
                '<div id="assignmentMessage"></div>' + // Container for success message
                '<button id="uploadButton" type="submit" class="btn btn-primary">Upload</button>' +
                '</form>');
        } else {
            $('#lectureContent').html('<p>No content available for this selection.</p>');

        }
    });
});