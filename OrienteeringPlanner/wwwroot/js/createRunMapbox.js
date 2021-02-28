var createRunCoords = {
    lat: 0.0,
    lng: 0.0
}

function RenderCreateRunMapboxComponent() {
    var elementExists = document.getElementById("createRunMap");

    if (elementExists) {
        var map = new mapboxgl.Map({
            accessToken: accessToken,
            container: 'createRunMap',
            style: 'mapbox://styles/thomasblok98/ckl6xv0gg19mk17s11nhdraet',
            zoom: 4,
            center: getLngLat()
        });

        var geocoder = new MapboxGeocoder({
            accessToken: accessToken,
            mapboxgl: mapboxgl,
            marker: false,
            placeholder: 'Search for location'
        });

        var marker = new mapboxgl.Marker()
            .setLngLat([53.20, 9.10])
            .setDraggable(true)
            .addTo(map);

        map.addControl(geocoder);

        map.addControl(new mapboxgl.NavigationControl());

        marker.on('drag', () => {
            createRunCoords.lat = marker.getLngLat().lat;
            createRunCoords.lng = marker.getLngLat().lng;
        });

        geocoder.on('result', function (e) {
            var searchCoords = {
                lng: e.result.geometry.coordinates[0],
                lat: e.result.geometry.coordinates[1]
            };

            createRunCoords.lat = searchCoords.lat;
            createRunCoords.lng = searchCoords.lng;

            marker.setLngLat(searchCoords);
        });

        map.on('click', function (e) {
            var clickCoords = e.lngLat;

            createRunCoords.lat = clickCoords.lat;
            createRunCoords.lng = clickCoords.lng;

            marker.setLngLat(clickCoords);
        });
    }
}

function GetRunCoords() {
    return createRunCoords;
}


function ResetFormDate() {
    document.getElementById("createRunForm").reset();
}