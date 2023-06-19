let x = 49.8230000;
let y = 19.0500000;

var map = L.map("map", {
    center: [x, y],
    zoom: 13,
});

L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
    maxZoom: 19,
    attribution:
        '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
}).addTo(map);

var markersLayer = new L.LayerGroup();	//layer contain searched elements

map.addLayer(markersLayer);

var controlSearch = new L.Control.Search({
	position: 'topright',
	layer: markersLayer,
	initial: false,
	zoom: 12,
	marker: false
}).addTo(map);

//var circle = L.circle([x, y], {
//    color: 'red',
//    fillColor: '#f03',
//    fillOpacity: 0.5,
//    radius: 900
//}).addTo(map);

//var polygon = L.polygon([
//    [x, y],
//    [50.1162706, 18.9741339],
//    [50.1164000, 18.9793800]
//]).addTo(map);

//var marker = L.marker([x, y], ).addTo(map);
//var marker = L.marker([50.1162706, 18.9741339], ).addTo(map);
//var marker = L.marker([50.1164000, 18.9793800], ).addTo(map);