
app.controller('mapaCtrl', function ($scope) {

    var vm = this

    var mymap = L.map('mapid').setView([-16.24, -47.91], 13);

    L.tileLayer('http://{s}.tile.osm.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(mymap);

    var marker = L.marker([-16.24, -47.91]).addTo(mymap);
    var marker = L.marker([-16.23, -47.89]).addTo(mymap);
    var marker = L.marker([-16.21, -47.915]).addTo(mymap);
    var marker = L.marker([-16.26, -47.90]).addTo(mymap);

    var circle = L.circle([-16.23, -47.92], {
        color: 'red',
        fillColor: '#f03',
        fillOpacity: 0.5,
        radius: 500
    }).addTo(mymap);

    var polygon = L.polygon([
    [-16.24, -47.91],
    [-16.23, -47.89],
    [-16.21, -47.915]
    ]).addTo(mymap);

    marker.bindPopup("<b>Hello world!</b><br>I am a popup.").openPopup();
    circle.bindPopup("I am a circle.");
    polygon.bindPopup("I am a polygon.");

    var popup = L.popup();

    function onMapClick(e) {
        popup
            .setLatLng(e.latlng)
            .setContent("You clicked the map at " + e.latlng.toString())
            .openOn(mymap);
    }

    mymap.on('click', onMapClick);

});