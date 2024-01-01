MyDevicesJs = (function () {
    let config = {
        myDevicesUrl: ''
    };

    function init(parameters) {
        $.extend(config, parameters);
    }

    function loadMyDevices() {
        $('#devices').load(config.myDevicesUrl);
    }

    return {
        init,
        loadMyDevices
    };
})();