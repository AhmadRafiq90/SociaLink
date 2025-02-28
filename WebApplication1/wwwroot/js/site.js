// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.querySelectorAll('.delete-user').forEach(function (deleteButton) {
    deleteButton.addEventListener('click', function (e) {
        e.preventDefault();
        var token = this.dataset.xsrftoken;

        if (confirm('Are you sure you want to delete this user?')) {
            fetch(this.dataset.url, {
                method: 'POST',
                headers: {
                    'XSRF-TOKEN': token,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: this.dataset.id })
            }).then(function (response) {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Failed to delete user');
                }
            });
        }
    });
});

document.querySelectorAll('.delete-society').forEach(function (deleteButton) {
    deleteButton.addEventListener('click', function (e) {
        e.preventDefault();

        var token = this.dataset.xsrftoken;
        console.log(this.dataset.id);
        if (confirm('Are you sure you want to delete this society?')) {
            fetch(this.dataset.url, {
                method: 'POST',
                headers: {
                    'XSRF-TOKEN': token,
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ id: this.dataset.id })
            }).then(function (response) {
                if (response.ok) {
                    location.reload();
                } else {
                    alert('Failed to delete society');
                }
            });
        }
    });
});

document.querySelectorAll('.join-society').forEach(function (deleteButton) {
    deleteButton.addEventListener('click', function (e) {
        e.preventDefault();

        var token = this.dataset.xsrftoken;

        if (confirm('Are you sure you want to join this society?')) {
            fetch(this.dataset.url, {
                method: 'POST',
                headers: {
                    'XSRF-TOKEN': token,
                    'Content-Type': 'application/json'
                }
                , body : JSON.stringify({ TOKEN: token })
            }).then(function (response) {
                if (response.ok) {
                    //alert('Your request has been forwarded');
                    //location.reload();
                } else {
                    alert('Failed to delete society');
                }
            });
        }
    });
});