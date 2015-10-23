$(document).ready(function (args) {
    //$('.blueberry').blueberry();
    StartUpAnimations();
    HookUpEventHandlers();
});

function StartUpAnimations() {
    $("#my-carousel").animate({ top: '60px' }, { speed: 5000, easing: 'easeOutBack' });
    $(".navbar-wrapper").animate({ top: '-10px' }, { speed: 5000, easing: 'easeOutBack' });
}

function HookUpEventHandlers() {
    NavBarEvents();
    JumbotronEvents();
    FormEvents();
}

function NavBarEvents() {
    $(".navbar li a").hover(function (a) {
        $(this).stop(true).animate({ paddingLeft: '25px' }, { speed: 400, easing: 'easeOutBack' });
    }, function (b) {
        $(this).stop(true).animate({ paddingLeft: '15px' }, { speed: 400, easing: 'easeOutBack' });
    });

    $(".dropdown .dropdown-menu a").hover(function (a) {
        $(this).stop(true).animate({ paddingLeft: '50px' }, { speed: 400, easing: 'easeOutBack' });
    }, function (b) {
        $(this).stop(true).animate({ paddingLeft: '30px' }, { speed: 400, easing: 'easeOutBack' });
    });

    $("#liAbout").click(function (args) {
        $("#AboutUs").collapse('show');
    });

    $("#liContact").click(function (args) {
        $("#ContactUs").collapse('show');
    });

    $("#liAndroidApps").click(function (args) {
        $("#AndroidApps").collapse('show');
    });

    $("#liWindowsApps").click(function (args) {
        $("#WindowsApps").collapse('show');
    });

    $("#liiOSApps").click(function (args) {
        $("#iOSApps").collapse('show');
    });

}
function JumbotronEvents() {
}
function FormEvents() {
    $("#contactForm").submit(function (args) {
        var name = $("#Name").val();
        var emailFrom = $("#EmailFrom").val();
        var subject = $("#Subject").val();
        var message = $("#Message").val();
        $.ajax({
            type: "Post",
            url: "/Home/SendEmail",
            data: { Name: name, EmailFrom: emailFrom, Subject: subject, Message: message },
            success: function (success) {

                $("#alertContainer").empty().append("<div class='alert alert-success alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button><strong>Success!</strong> Thanks for reaching to us. We will get back to you shortly!</div>");
            },
            error: function (error) {
                $("#alertContainer").empty().append("<div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert'><span aria-hidden='true'>&times;</span><span class='sr-only'>Close</span></button><strong>OOPS!</strong> Unable to contact! Please try again later.</div>");
            }
        });
        $("#contactUsModal").modal('hide');
        return false;
    });
}