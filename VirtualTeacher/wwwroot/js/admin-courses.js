$(document).ready(function () {
    function fetchCourses(pageNumber, sortBy, sortOrder) {
        $.ajax({
            url: '/Admin/GetCourses',
            method: 'GET',
            data: {
                page: pageNumber,
                sortBy: sortBy,
                sortOrder: sortOrder
            },
            success: function (response) {
                updateTable(response.courses);
                updatePagination(response.totalPages, response.currentPage);
            },
            error: function (xhr, status, error) {
                console.error('Error:', error);
            }
        });
    }

    function updateTable(courses) {
        $('#table-body').empty();
        $.each(courses, function (index, course) {
            $('#table-body').append('<tr><td>' + course.id + '</td><td>' + course.title + '</td><td>' + course.creator.email + '</td><td>' + course.startDate + '</td><td>' + course.isPublic + '</td></tr>');
        });
    }

    function updatePagination(totalPages, currentPage) {
        $('#pagination').empty();
        for (var i = 1; i <= totalPages; i++) {
            var activeClass = (i === currentPage) ? 'active' : '';
            $('#pagination').append('<li class="page-item ' + activeClass + '"><a class="page-link" href="#" data-page="' + i + '">' + i + '</a></li>');
        }
    }

    $('#pagination').on('click', '.page-link', function (e) {
        e.preventDefault();
        var page = $(this).data('page');
        fetchCourses(page);
    });

    $('#table-header').on('click', 'th[data-sort]', function () {
        var sortBy = $(this).data('sort');
        var sortOrder = $(this).hasClass('asc') ? 'desc' : 'asc';
        fetchCourses(1, sortBy, sortOrder);
    });

    fetchCourses(1); // Initial data fetch
});
