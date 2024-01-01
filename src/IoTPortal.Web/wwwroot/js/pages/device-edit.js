DeviceEditJs = (function () {
    let timeout;

    function init() {
        $('#userDatalist').on('keyup', function () {
            if (timeout != null) {
                clearTimeout(timeout);
            }

            timeout = setTimeout(fetchUsernames, 1500);
        });
    }

    function fetchUsernames() {
        const search = $('#userDatalist').val().trim();
        if (search.length === 0) {
            return;
        }

        $.get('/api/user/find/username', { search })
            .done(function (usernames) {
                $('#userDatalistOptions').empty();
                usernames.forEach(username => {
                    $('#userDatalistOptions').append(`<option value=\"${username}\">`);
                });
            })
            .always(function () {
                timeout = null;
            });
    }

    return {
        init
    };
})();