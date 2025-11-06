$(function () {

    //Events
    $('.input-container input').on("input change", validateCreateForm);


    //Helper functions
    function validateCreateForm() {

        let valid = true;

        //Ignore the checkbox (always valid)
        $('.input-container input[type="text"], .input-container input[type="date"]')
            .each(function () {

                const value = $(this).val().trim();
                console.log("VALUE:", value);

                if (value === "") {
                    valid = false;
                }
            });
       
        if (valid) {
            $('#create-btn').removeClass('disabled');
        } else {
            $('#create-btn').addClass('disabled');
        }
    }
});
