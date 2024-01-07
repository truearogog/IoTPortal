DeviceJs = (function () {
    function init(deviceId) {
        initChart(deviceId);
    }

    function initChart(deviceId) {
        $.when(
            $.get('/api/measurement/types', { deviceId }),
            $.get('/api/measurement/groups', { deviceId }))
            .done(function (typesResponse, groupsResponse) {
                const types = typesResponse[0];
                const groups = groupsResponse[0];

                const data = {
                    datasets: []
                };

                for (let i = 0; i < types.length; i++) {
                    const dataset = {
                        label: types[i].name + ', ' + types[i].unit,
                        borderColor: types[i].color,
                        data: groups.map(function (group) {
                            return {
                                x: Date.parse(group.created),
                                y: group.measurements[i]
                            };
                        }),
                        pointStyle: false
                    };

                    data.datasets.push(dataset);
                }

                const config = {
                    type: 'line',
                    data: data,
                    options: {
                        responsive: true,
                        scales: {
                            x: {
                                type: 'time'
                            }
                        }
                    }
                };
                
                new Chart(document.getElementById('deviceChart'), config);
            });
    }

    return {
        init
    };
})();