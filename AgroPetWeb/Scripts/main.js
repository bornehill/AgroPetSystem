var myCenter = new google.maps.LatLng(21.879777,-102.310928);

function initialize() {
var mapProp = {
center:myCenter,
zoom:19,
scrollwheel:false,
draggable:true,
mapTypeId:google.maps.MapTypeId.ROADMAP
};

	if (document.getElementById("googleMap") != null) {
			var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);

			var marker = new google.maps.Marker({
					position: myCenter,
			});

			marker.setMap(map);
	}
}

$(document).ready(function(){
    google.maps.event.addDomListener(window, 'load', initialize);
    // Add smooth scrolling to all links in navbar + footer link
    $("footer a[href='#myPage']").on('click', function(event) {

    // Prevent default anchor click behavior
    event.preventDefault();

    // Store hash
    var hash = this.hash;

    // Using jQuery's animate() method to add smooth page scroll
    // The optional number (900) specifies the number of milliseconds it takes to scroll to the specified area
    $('html, body').animate({
    scrollTop: $(hash).offset().top
    }, 900, function(){

    // Add hash (#) to URL when done scrolling (default click behavior)
    window.location.hash = hash;
    });
    });
    
    /*Galery of images item*/
    
    $('#gallery').simplegallery({
            galltime : 400,
            gallcontent: '.content',
            gallthumbnail: '.thumbnail',
            gallthumb: '.thumb'
        });
    /*end galery of images item*/
    
    /*slider of articles*/
    $('#myCarousel').carousel({
	interval: 10000
	})
    
    $('#myCarousel').on('slid.bs.carousel', function() {
    	//alert("slid");
	});
    /*end slider of articles*/
});

$(function () {
	$("#menu").menu();
});