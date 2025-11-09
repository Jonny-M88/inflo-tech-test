$(function () {

    //CONSTANTS
    const $logRows = $('.table-responsive tbody tr');

    //EVENT HANDLERS
    $('.input-container input').on("input change", validateUserForm);

    $('#filter-log-btn').on('click', filterLogs);
    $('#clear-log-btn').on('click', clearFilters);


    //HELPER FUNCTIONS
    //Validates form inputs
    function validateUserForm() {

        updateCurrentUser();

        const inputsValid = [];
        $('.input-container input[type="text"], .input-container input[type="date"], .input-container input[type="email"]').each(function () {

            const input = $(this);
            const fieldName = input.attr('name');
            const value = input.val().trim();

            switch (fieldName) {
                case "Forename":
                    inputsValid.push(checkStringValid(value));
                    break;
                case "Surname":
                    inputsValid.push(checkStringValid(value));
                    break;
                case "Email":
                    inputsValid.push(checkEmailValid(value));
                    break;
                case "DateOfBirth":
                    inputsValid.push(checkDateValid(value));
                    break;
            }
        });

        const changesMade = checkUserForChanges();
        const allValid = !inputsValid.includes(false);

        $('#submit-user-btn').prop('disabled', !(allValid && changesMade));
    }
    //Updates values on the current user
    function updateCurrentUser() {

        $('.input-container input[type="text"], .input-container input[type="date"], .input-container input[type="email"]').each(function () {

            const input = $(this);
            const fieldName = input.attr('name');
            let value = input.val().trim();
            currentUser[fieldName] = value;
        });

        const checkbox = $('.input-container input[type="checkbox"]');
        if (checkbox.length) {
            currentUser.IsActive = checkbox.prop('checked');
        }
    }
    //Checks string input value is valid
    function checkStringValid(stringValue) { return stringValue.length >= 2; }
    //Checks an email input value is valid (Regex)
    function checkEmailValid(emailValue) { return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailValue); }
    //Checks a date value from a date picker value is valid
    function checkDateValid(dateValue) { return dateValue != null && dateValue.length > 0; }
    //Checks to make sure a change has been made to the current user
    function checkUserForChanges() {
        return JSON.stringify(currentUser) !== JSON.stringify(user);
    }
    // Filter the log rows based on the date picker values
    function filterLogs() {

        const fromDate = parseDate($('#filter-from').val());
        const toDate = parseDate($('#filter-to').val());

        $logRows.each(function () {

            //Get the date value from each row
            const $row = $(this);
            const actionDateStr = $row.find('.action-date').text().trim();

            const actionDate = new Date(actionDateStr);

            //determine if the row should be hidden or not
            let show = true;
            if (fromDate && actionDate < fromDate) show = false;
            if (toDate && actionDate > toDate) show = false;

            $row.toggle(show);
        });

    }
    // Clear log filters
    function clearFilters() {
        $('#filter-from').val('');
        $('#filter-to').val('');
        $logRows.show();
    }
    //Formats the date
    function parseDate(dateStr) {

        if (!dateStr) return null;
        const parts = dateStr.split('-');
        return new Date(parts[0], parts[1] - 1, parts[2]);
    }
});
