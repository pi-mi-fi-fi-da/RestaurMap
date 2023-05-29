let x = 50.1119000;
let y = 18.9842351;

var map = L.map("map", {
    center: [x, y],
    zoom: 12,
});

L.tileLayer("https://tile.openstreetmap.org/{z}/{x}/{y}.png", {
    maxZoom: 19,
    attribution:
        '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>',
}).addTo(map);

var circle = L.circle([x, y], {
    color: 'red',
    fillColor: '#f03',
    fillOpacity: 0.5,
    radius: 900
}).addTo(map);

var polygon = L.polygon([
    [x, y],
    [50.1162706, 18.9741339],
    [50.1164000, 18.9793800]
]).addTo(map);

var marker = L.marker([x, y], ).addTo(map);
var marker = L.marker([50.1162706, 18.9741339], ).addTo(map);
var marker = L.marker([50.1164000, 18.9793800], ).addTo(map);