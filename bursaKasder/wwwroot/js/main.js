/*===================================================
Project:   Forsa - Multipurpose HTML5 Website Template
Auther: Mohamed Amin [AminThemes]
Auther-URI:https://www.templatemonster.com/authors/aminthemes/
Version:    1.0.01
Created:   1 Jan 2023
Template Description: Multipurpose HTML5 Website Template
====================================================*/

//GLOBAL VARIBALES
var //selector vars
    main_window = $(window),
    root = $("html, body"),
    bdyOnePage = $("body.landing-page-demo "),
    pageHeader = $("#page-header"),
    navMain = $("nav.main-navbar"),
    navMenuWraper = $(".navbar-menu-wraper"),
    hasSubMenu = $(".has-sub-menu"),
    onePage_navLink = $(
        ".landing-page-demo .main-navbar .nav-link, .landing-page-demo .goto-link"
    ),
    pageHero = $("#page-hero"),
    backToTopButton = $(".back-to-top"),
    heroVegasSlider = $(".hero-vegas-slider"),
    heroSwiperSlider = ".hero-swiper-slider .swiper-container",
    tabLink = $(".ma-tabs .tabs-links .tab-link"),
    togglerLink = $(".ma-tabs .toggler-links .toggler"),
    portfolioGroup = $(".portfolio .portfolio-group"),
    // Measurements vars
    navMainHeight = navMain.innerHeight(),
    pageHeroHeight = pageHero.innerHeight(),
    //class Names Strings vars

    hdrStandOut = "header-stand-out",
    inputHasText = "has-text",
    // condetionals vars
    counterShowsUp = false;

$(function () {
    ("use strict");
    /*-----------------  START GENERAL FUNCTIONS  -----------------*/

    // function to fire the conter plugin
    function fireCounter() {
        if ($(".stats-counter").length) {
            if (jQuery().countTo && counterShowsUp === false) {
                var pos = $(".stats-counter").offset().top;

                if (main_window.scrollTop() + main_window.innerHeight() - 50 >= pos) {
                    $(".counter").countTo();
                    counterShowsUp = true;
                }
            }
        }
    }

    /*-----------------  END GENERAL FUNCTIONS  -----------------*/

    /*----------------- Start Calling global function -----------------*/

    /*this functions must fires on the page ready to load*/

    // to fire the counter when its section appear on screen
    fireCounter();

    /*----------------- End Calling global function -----------------*/

    // to remove class from navbar if the page refreshed and the scrolltop of the window > 50px
    if (main_window.scrollTop() > 100) {
        $(pageHeader).toggleClass(hdrStandOut);
        $(backToTopButton).toggleClass("show");
    }

    /* ----------------- End page loading Actions * ----------------- */

    /* ----------------- Start onClick Actions * ----------------- */

    //  Start Smooth Scrolling To page Sections
    $(onePage_navLink).on("click", function (e) {
        var link = $(this).attr("href");
        var currentMainNavHeight = navMain.innerHeight();

        if (link.charAt(0) === "#") {
            e.preventDefault();
            var target = this.hash;
            $(root).animate(
                {
                    scrollTop: $(target).offset().top - currentMainNavHeight + 1,
                },
                500
            );
        }
        if (!$(this).parent(".nav-item").hasClass("has-sub-menu")) {
            // to close the menu after going to crossponding section
            if ($(".navbar-menu-wraper").hasClass("show-menu")) {
                $(".navbar-menu-wraper").removeClass("show-menu");
            }
            // to change the menu toggler buttn icon
            if ($(".menu-toggler-btn").hasClass("close-menu-btn")) {
                $(".menu-toggler-btn").removeClass("close-menu-btn");
            }
        }
    });

    //End Smooth Scrolling To page Sections

    $(".navbar-nav").on("click", function (e) {
        e.stopPropagation();
    });

    //  open and close menu btn
    $(".menu-toggler-btn, .navbar-menu-wraper ").on("click", function () {
        $(".menu-toggler-btn").toggleClass("close-menu-btn");
        navMenuWraper.toggleClass("show-menu");

        //  add/remove  .header-stand-out  class to .main-navbar when menu-toogler-clicked

        //  if the menu is opened
        if ($(".show-menu").length) {
            // add .header-stand-out class to .main-nav
            if (!pageHeader.hasClass(hdrStandOut))
                $(pageHeader).addClass(hdrStandOut);
        } else {
            // remove .header-stand-out class to .main-nav in case the window scrolltop less than 50px
            if (pageHeader.hasClass(hdrStandOut) && main_window.scrollTop() < 50)
                $(pageHeader).removeClass(hdrStandOut);
        }
    });

    //showing navbar sub-menus
    hasSubMenu.on("click", function (e) {
        e.stopPropagation();
        if (!(main_window.innerWidth() > 1199)) {
            $(this).children(".sub-menu").slideToggle();
        }
    });

    // Start Smooth Scrolling To Window Top When Clicking on Back To Top Button
    $(backToTopButton).on("click", function () {
        root.animate(
            {
                scrollTop: 0,
            },
            1000
        );
    });
    // End Smooth Scrolling To Window Top When Clicking on Back To Top Button

    /* ----------------- End onClick Actions ----------------- */

    // Start  Toggler Tabs

    //When click on the left Switch btn
    $(".switch-btn ").on("click", function () {
        // 1-) add .active class to the pressed btn & remove it from sibilings
        $(this).addClass("active").siblings().removeClass("active");

        // 2-) show the wanted tab
        var target = $(this).attr("data-target");
        var currentSection = $(this).parents(".ma-tabs");
        $(currentSection)
            .find(target)
            .addClass("visibale-tab")
            .siblings(".tab-content")
            .removeClass("visibale-tab");
    });

    // Do the same as the clicked switch-btns but when press on the checkbox toggler it self
    $(".toggle-btn").on("click", function () {
        let target = "";
        if ($(this).prop("checked")) {
            // 1-) add .active class to the pressed btn & remove it from sibilings
            $(this)
                .siblings(".toggle-left")
                .removeClass("active")
                .siblings(".toggle-right")
                .addClass("active");

            target = $(this).siblings(".toggle-right").attr("data-target");
        } else {
            // 1-) add .active class to the pressed btn & remove it from sibilings
            $(this)
                .siblings(".toggle-left")
                .addClass("active")
                .siblings(".toggle-right")
                .removeClass("active");
            target = $(this).siblings(".toggle-left").attr("data-target");
        }

        // 2-) show the wanted tab
        var currentSection = $(this).parents(".ma-tabs");
        $(currentSection)
            .find(target)
            .addClass("visibale-tab")
            .siblings(".tab-content")
            .removeClass("visibale-tab");
    });

    // End Toggler Tabs

    /*  -----------------* Start skills Bars   -----------------*/
    $(window).on("scroll", function () {
        $(".skills .skill .skill-bar .bar").each(function () {
            let barOriginalPosition = $(this).offset().top + $(this).outerHeight();
            let barCurrPosition = $(window).scrollTop() + $(window).height();
            let widthValue = $(this).attr("data-skill-val");
            if (barCurrPosition > barOriginalPosition) {
                $(this).css({
                    width: widthValue + "%",
                });
            }
        });
    });
    /*  -----------------* End skills Bars   -----------------*/

    /* ----------------- Start onScroll Actions ----------------- */

    main_window.on("scroll", function () {
        if ($(this).scrollTop() > 50) {
            //show back to top btn
            backToTopButton.addClass("show");
        } else {
            //hide back to top btn
            backToTopButton.removeClass("show");
        }
        /*----------------------------------------------------
        * To add/remove a class that makes navbar stands out
        * by changing its colors to the opposit colors
        -------------------------------------------------*/
        if ($(this).scrollTop() > 50 || $(".show-menu").length) {
            if (!$(pageHeader).hasClass(hdrStandOut))
                $(pageHeader).addClass(hdrStandOut);
        } else if ($(this).scrollTop() < 50 && !$(".show-menu").length) {
            if ($(pageHeader).hasClass(hdrStandOut))
                $(pageHeader).removeClass(hdrStandOut);
        }

        // to make sure the counter will start counting whit its section apear on the screen
        fireCounter();
    });

    /* ----------------- End onScroll Actions ----------------- */
    /* --------------------------
      Start Vendors plugins options  
      ----------------------------*/

    /* Start  wow.js  Options */

    var wow = new WOW({
        animateClass: "animated",
    });
    wow.init();

    // set the swiper slides & hero sections bg image
    var slideBg = $(
        ".hero-swiper-slider .swiper-container .swiper-slide .slider-bg"
    );
    slideBg.each(function () {
        if ($(this).attr("data-background")) {
            $(this).css(
                "background-image",
                "url(" + $(this).data("background") + ")"
            );
        }
    });
    /* Start Swiper Options */

    //initialize swiper [Hero Section]  //parallax slider
    if ($(".hero-swiper-slider.slider-parallax .swiper-container").length) {
        var heroSlider = new Swiper(
            ".hero-swiper-slider.slider-parallax .swiper-container",
            {
                speed: 1000,
                parallax: true,
                loop: true,
                reverseDirection: true,
                autoplay: {
                    delay: 5000,
                    disableOnInteraction: true,
                },
                pagination: {
                    el: ".hero-swiper-slider.slider-parallax .swiper-pagination",
                    type: "bullets",
                    clickable: true,
                },

                navigation: {
                    nextEl: ".hero-swiper-slider.slider-parallax .swiper-button-next",
                    prevEl: ".hero-swiper-slider.slider-parallax .swiper-button-prev",
                },
                on: {
                    init: function () {
                        var thisSlider = this;
                        for (var i = 0; i < thisSlider.slides.length; i++) {
                            $(thisSlider.slides[i])
                                .find(".slider-bg")
                                .attr({
                                    "data-swiper-parallax": 0.65 * thisSlider.width,
                                });
                        }
                    },
                    resize: function () {
                        this.update();
                    },
                },
            }
        );
    }

    // initialize swiper [Testimonials with ONE Column]
    if ($(".testimonials-1-col .swiper-container").length) {
        var testimonialsSlider_1 = new Swiper(
            ".testimonials-1-col .swiper-container",
            {
                // Optional parameters
                speed: 500,
                loop: true,
                grabCursor: true,
                slidesPerView: 1,
                spaceBetween: 50,

                delay: 5000,
                autoplay: {
                    delay: 5000,
                },
                breakpoints: {
                    991: {
                        slidesPerView: 1,
                    },
                },

                navigation: {
                    nextEl: ".testimonials-1-col .swiper-button-next",
                    prevEl: ".testimonials-1-col .swiper-button-prev",
                },
            }
        );
    }

    // initialize swiper [Testimonials with TOW Columns]
    if ($(".testimonials-2-col .swiper-container").length) {
        var testimonialsSlider_2 = new Swiper(
            ".testimonials-2-col .swiper-container",
            {
                // Optional parameters
                speed: 500,
                loop: true,
                grabCursor: true,
                slidesPerView: 2,
                spaceBetween: 20,
                centeredSlides: true,
                delay: 5000,
                autoplay: {
                    delay: 5000,
                },
                breakpoints: {
                    991: {
                        slidesPerView: 1,
                    },
                },

                navigation: {
                    nextEl: ".testimonials-2-col .swiper-button-next",
                    prevEl: ".testimonials-2-col .swiper-button-prev",
                },
            }
        );
    }

    // initialize swiper [Testimonials with THREE Column]
    if ($(".testimonials-3-col .swiper-container").length) {
        var testimonialsSlider_3 = new Swiper(
            ".testimonials-3-col .swiper-container",
            {
                // Optional parameters
                speed: 600,
                loop: true,
                grabCursor: true,
                slidesPerView: 3,
                spaceBetween: 10,
                delay: 5000,
                autoplay: {
                    delay: 5000,
                },
                breakpoints: {
                    991: {
                        slidesPerView: 1,
                    },
                },

                navigation: {
                    nextEl: ".testimonials-3-col .swiper-button-next",
                    prevEl: ".testimonials-3-col .swiper-button-prev",
                },
            }
        );
    }

    //initialize swiper [clients Section]
    if ($(".our-clients .swiper-container").length) {
        var partenersSlider = new Swiper(".our-clients .swiper-container", {
            // Optional parameters
            speed: 600,
            loop: true,
            spaceBetween: 30,
            grabCursor: true,

            delay: 5000,
            autoplay: {
                delay: 5000,
            },
            slidesPerView: 6,
            breakpoints: {
                991: {
                    slidesPerView: 3,
                    spaceBetween: 20,
                },
            },
        });
    }

    //initialize swiper [single-post page]
    if ($(".post-main-area .post-featured-area .swiper-container").length) {
        var partenersSlider = new Swiper(
            ".post-main-area .post-featured-area .swiper-container",
            {
                // Optional parameters
                slidesPerView: 1,
                grabCursor: true,
                spaceBetween: 0,
                loop: true,
                pagination: {
                    el: ".swiper-pagination",
                    clickable: true,
                },
                navigation: {
                    nextEl: ".swiper-button-next",
                    prevEl: ".swiper-button-prev",
                },
            }
        );
    }

    if ($(".portfolio-slider .swiper-container").length) {
        var swiperPortfolioSlider = new Swiper(
            ".portfolio-slider .swiper-container",
            {
                spaceBetween: 10,
                speed: 600,
                loop: true,
                centeredSlides: true,
                slidesPerView: 3,
                autoplay: {
                    delay: 5000,
                },
                breakpoints: {
                    576: {
                        slidesPerView: 1,
                        spaceBetween: 10,
                    },
                    768: {
                        slidesPerView: 2,
                        spaceBetween: 10,
                    },
                    991: {
                        slidesPerView: 3,
                        spaceBetween: 10,
                    },
                },

                pagination: {
                    el: ".portfolio-slider .swiper-pagination",
                    type: "progressbar",
                },

                navigation: {
                    nextEl: ".portfolio-slider .swiper-button-next",
                    prevEl: ".portfolio-slider .swiper-button-prev",
                },
            }
        );
    }

    /* Start fancybox Options */
    // portfolio fancy box initializer

    if ($("*").fancybox) {
        $().fancybox({
            selector: '[data-fancybox=".filter"]:visible',
            loop: true,
            buttons: ["zoom", "close"],
        });
    }

    /* Start bootstrap Scrollspy Options  */
    /*-------------------------------------*/
    // //on one page demos only
    if (navMain) {
        $(bdyOnePage).scrollspy({
            target: navMain,
            offset: navMain.innerHeight() + 1,
        });
    }

    /* Start isotope Options */
    /*-------------------------------------*/
    $(".portfolio .portfolio-btn").on("click", function () {
        $(this).addClass("active").siblings().removeClass("active");

        var $filterValue = $(this).attr("data-filter");
        portfolioGroup.isotope({
            filter: $filterValue,
        });
    });

    /*----------------- End Vendors plugins options ----------------- */

    /*************Start Form Functionality************/
    var contactForm = $("#contact-us-form"),
        iputWraper = $("form.main-form .input-wraper"),
        textInput = $("form.main-form .text-input"),
        userName = $("#user-name"),
        userEmail = $("#user-email"),
        msgSubject = $("#msg-subject"),
        msgText = $("#msg-text"),
        submitBtn = $("#submit-btn"),
        errorMsg = $(".error-msg"),
        isValidInput = false,
        isValidEmail = false;

    function ValidateNotEmptyInput(input, errMsg) {
        if (input.val().trim() === "") {
            $(input).siblings(".error-msg").text(errMsg).css("display", "block");
            isValidInput = false;
        } else {
            $(input).siblings(".error-msg").text("").css("display", "none");
            isValidInput = true;
        }
    }

    function validateEmailInput(emailInput) {
        var pattern =
            /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

        if (pattern.test(emailInput.val()) === false) {
            $(emailInput)
                .siblings(".error-msg")
                .text("Please Enter a valid Email")
                .css("display", "block");
            isValidEmail = false;
        } else {
            $(emailInput).siblings(".error-msg").text("").css("display", "none");
            isValidEmail = true;
        }

        // return pattern.test(emailInput);
    }

    submitBtn.on("click", function (e) {
        e.preventDefault();

        ValidateNotEmptyInput(userName, "Please Enter Your Name");
        ValidateNotEmptyInput(userEmail, "Please Enter Your Email");
        ValidateNotEmptyInput(msgSubject, "Please Enter Your subject");
        ValidateNotEmptyInput(msgText, "Please Enter Your Message");
        validateEmailInput(userEmail);

        if (isValidInput && isValidEmail) {
            $.ajax({
                type: "POST",
                url: contactForm.attr("action"),
                data: contactForm.serialize(),

                success: function (data) {
                    $(".done-msg")
                        .text("Thank you, Your Message Was Received!")
                        .toggleClass("show");
                    setTimeout(function () {
                        $(".done-msg").text("").toggleClass("show");
                    }, 4000);
                    contactForm[0].reset();
                },
            });
            return false;
        }
    });

    if (textInput.length) {
        if (textInput.val().trim() !== "")
            textInput.parent().addClass(inputHasText);
        else textInput.parent().removeClass(inputHasText);

        //check if the form input has data or not while focusing out
        //from the input to set the label
        //in the right place by the css rules.
        textInput.on("focusout", function () {
            if ($(this).val().trim() !== "") $(this).parent().addClass(inputHasText);
            else $(this).parent().removeClass(inputHasText);
        });
    }

    /*************End Form Functionality************/

    /*----------------- Start page loading Actions -----------------  */

    // $(window).on("load", function () {
    //Fire the isotope plugin
    if (jQuery().isotope) {
        portfolioGroup.isotope({
            // options
            itemSelector: ".portfolio-item",
            layoutMode: "masonry",
            percentPosition: false,
            filter: "*",
            stagger: 30,
            containerStyle: null,
        });
    }

    //loading screen
    $("#loading-screen").fadeOut(500);
    $("#loading-screen").remove();

    // check if the loaded page have [dat-splitting] attr so the let the page fire splitting.js plugin  ;
    if (!(typeof window.Splitting === "undefined")) {
        if ($("[data-splitting]").length) {
            Splitting();
        }
    }
});
/*----------------- End Loading Event functions -----------------*/