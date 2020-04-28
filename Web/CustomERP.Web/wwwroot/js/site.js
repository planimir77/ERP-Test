// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$("#menu-toggle").click(function(e) {
//    e.preventDefault();

//    $("#wrapper").toggleClass("active");
//});

function menuToggle() {

    var pic = document.getElementById("mySidepanel").style.left;
    
    if (pic === "-245px" || pic === ""){/*  collapse */
        document.getElementById("mySidepanel").style.left = "-40px";
    }
    else {/*  expand menu */
        document.getElementById("mySidepanel").style.left = "-245px" ;
    }
};

function openNav() {
    var pic = document.getElementById("mySidepanel").style.left = "-40px";
};

function closeNav() {
    document.getElementById("mySidepanel").style.left = "-245px" ;
};
