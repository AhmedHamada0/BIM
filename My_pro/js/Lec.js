

$(document).ready(function(){
        $('.tabs > div.rightTabs > li').click(function(){
        $('#TabsContents > .tab.active').addClass('leave');
        setTimeout(function(){
            $('#TabsContents > .tab.leave').removeClass('leave');
        },300);
        $('.tabs > div.rightTabs > li').removeClass('active');
        $(this).addClass('active');
        $('#TabsContents > .tab').removeClass('active');
        $('#TabsContents > .tab[data-tab='+$(this).data('filter')+']').addClass('active');
        $('#TabsContents').css({"height":$('#TabsContents > .tab[data-tab='+$(this).data('filter')+']').height() + 60});
    });
});


$(document).ready(function(){
            $('.serversList > li').click(function(){
                $(".serversList > li").removeClass('active');
                $(this).addClass('active');
            });
        });

$(document).ready(function () {
                $('.serversList > li').click(function () {
                  $(".serversList > li").removeClass('active');
                  $(this).addClass('active');
                  var xxx = $(this).data('server');
                  $(".Vi").attr("src", xxx);
                });
            });
            function Count(c) {
              $.ajax({
                  url: "/Levels/Count",
                  data: { 'c': c },
                  type: 'GET',
                  success: function (result) {
                  },
                  error: function (result) {
                  }
                });
            }






            $(document).ready(function(){
    $('.filter > a:first-child').addClass('active');
    $('.tabsList > div.tab'+$('.filter > a:first-child').data('tab')).addClass('active');
});