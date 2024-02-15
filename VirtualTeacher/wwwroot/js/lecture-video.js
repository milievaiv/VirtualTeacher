    $(document).ready(function() {
        $('.collapse').on('show.bs.collapse', function() {
            $(this).siblings('.card-header').find('.btn-link').attr('aria-expanded', 'true');
        }).on('hide.bs.collapse', function() {
            $(this).siblings('.card-header').find('.btn-link').attr('aria-expanded', 'false');
        });
      
        $(document).on('click', '.card-header', function () {
            var videoUrl = $(this).next('.collapse').find('.video-selector').data('video-url');
            if(videoUrl) {
                var videoId = videoUrl.split('v=')[1];
                var ampersandPosition = videoId.indexOf('&');
                if (ampersandPosition != -1) {
                    videoId = videoId.substring(0, ampersandPosition);
                }
                var embedUrl = "https://www.youtube.com/embed/" + videoId;
                $('#lectureVideo').attr('src', embedUrl);
            }
        });
    });