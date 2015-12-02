$(function () {
    $('#rating-container > .rating-star').mouseenter(function () {
        $(this).prevAll().andSelf().addClass("rating-hover")
        $(this).nextAll().removeClass("rating-hover").addClass("no-rating");
        $('.meaning').fadeIn('fast');
        // $(this).removeClass("rating-chosen");
    });
    $('#rating-container > .rating-star').mouseleave(function () {
        $(this).nextAll().removeClass("no-rating");
    });
    $('#rating-container').mouseleave(function () {
        $('.rating-star').removeClass("rating-hover");
        $('.meaning').fadeOut('fast');
    });

    $('#rating-container > .rating-star').click(function () {
        $(this).prevAll().andSelf().addClass("rating-chosen");
        $(this).nextAll().removeClass("rating-chosen");

    });

    $("#1-star").hover(function () {
        $('.meaning').text('1');
    });
    $("#2-star").hover(function () {
        $('.meaning').text('2');
    });
    $("#3-star").hover(function () {
        $('.meaning').text('3');
    });
    $("#4-star").hover(function () {
        $('.meaning').text('4');
    });
    $("#5-star").hover(function () {
        $('.meaning').text('5');
    });
});