$(function () {

    //EVENT HANDLERS
    $('.input-container input').on("input change", validateUserForm);


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
});
