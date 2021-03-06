$(document).ready(function () {

    var owl = $("#slider");


    var users;
    var needCount = 1000;
    var data = {};

    owl.owlCarousel({
        navigation: false,
        pagination: true,
        slideSpeed: 300,
        paginationSpeed: 400,
        singleItem: true,
        autoPlay: 5000,
        addClassActive: true,
        afterAction: changeFeatureImg
    });


    setTimeout(function () { $('.feature-img-bg').fadeIn() }, 400);
    setTimeout(function () { $('.feature-img').fadeIn() }, 400);


    $('#slider').animate({ "opacity": "1", "left": 0 }, 400).css({ "display": "block" });
    $('.box-icon').addClass("active")

    setTimeout(function () { $('.stat-content').fadeIn() }, 500);
    $('.github-icon').fadeIn();
    $('.socials').fadeIn();

    $.ajax({
        type: "GET",
        url: "/api/status/GetStatus", //this section should get status fom API
        dataType: "json",
        success: function (data) {
            debugger;
            countTo($("#f-user-count"), data.registered);
            $('.section-stat').viewportChecker({
                callbackFunction: function (elem, action) {
                    //countTo($("#f-user-count"), data.registered);
                    //countTo( $("#f-jobs-count"), data.jobCount );
                    //countTo( $("#f-freelancer-count"), data.freelancerCount);
                },
                offset: "35%"
            });


            //$('.targets').viewportChecker({
            //    callbackFunction: function (elem, action) {

            //        if ($("#f-user-count").text() == "") {
            //            countTo($("#f-user-count"), data.userCount);
            //        }
            //        if ($("#f-jobs-count").text() == "") {
            //            countTo($("#f-jobs-count"), data.jobCount);
            //        }
            //        if ($("#f-freelancer-count").text() == "") {
            //            countTo($("#f-freelancer-count"), data.freelancerCount);
            //        }
            //        countTo($("#p-user-count"), data.userCount);
            //        users = data.userCount;
            //        setBar(data.userCount, needCount);
            //    },
            //    offset: "20%"
            //});

        }
    });



    function changeFeatureImg() {
        switch (this.owl.currentItem) {
            case 0:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/0.jpg');
                }).fadeIn(200);
                break;
            case 1:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/1.jpg');
                }).fadeIn(200);
                break;
            case 2:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/2.jpg');
                }).fadeIn(200);
                break;
            case 3:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/3.jpg');
                }).fadeIn(200);
                break;
            case 4:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/4.jpg');
                }).fadeIn(200);
                break;
            case 5:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/5.jpg');
                }).fadeIn(200);
                break;
            case 6:
                $('#feature-img').fadeOut(200, function () {
                    $('#feature-img').attr('src', '/themplate/img/features/6.jpg');
                }).fadeIn(200);
                break;
        };
    };




    function countTo(elem, to) {
        var $this = elem;

        jQuery({ Counter: 0 }).animate({ Counter: to || 0 }, {
            duration: 1000,
            easing: 'swing',
            step: function () {
                $this.text(Math.ceil(this.Counter));
            }
        });
    }

    function setBar(to, needCount) {

        var val = Math.floor((users / needCount) * 100);
        var $this = $('.progress .bar');
        var doneSettingBar = false;
        var complete = false;
        $this.each(function () {

            jQuery({ Counter: 0 }).animate({ Counter: val || 0 }, {
                duration: 1000,
                easing: 'swing',
                step: function () {
                    var success = false;
                    width = (Math.ceil(this.Counter));
                    doneSettingBar = width >= 100;
                    if (!complete) {
                        if (doneSettingBar) {
                            $this.attr('style', 'width:100%;background-position:0 100%;background-color: #40b95a');
                            $("#open-source-success").fadeTo('slow', 1);
                            complete = true;
                        } else if (width < 100) {
                            $this.attr('style', 'width:' + width + '%;background-position:0 ' + width + '%');
                        }
                    }
                }
            });

        });
        $("#user-percentage").text(val);
    }

});

$(window).load(function () {
    $(".owl-item").not(".active").hide();
});
