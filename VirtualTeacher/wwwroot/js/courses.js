$(document).ready(function () {
    $(".nav-link").click(function (e) {
        e.preventDefault(); // Prevent the default anchor behavior

        var selectedTopic = $(this).text().trim();
        console.log("Nav item clicked - Selected Topic:", selectedTopic);

        loadCourses(selectedTopic);
    });

    function loadCourses(topic) {
        console.log("Loading courses for topic:", topic);
        $("#coursesContainer").html("<p>Loading courses for: " + topic + "...</p>");

        var url = '/Courses/GetCoursesByTopic';
        $.ajax({
            url: url,
            type: 'GET',
            data: { topic: topic },
            success: function (data) {
                console.log("Courses loaded successfully for topic:", topic);
                $("#coursesContainer").html(data);
            },
            error: function (xhr, status, error) {
                console.error("Error loading courses for topic: " + topic + "; Error:", error);
                $("#coursesContainer").html("<p>Failed to load courses for: " + topic + "</p>");
            }
        });
    }
});