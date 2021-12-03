$(document).ready(function (parametars) {

        $('button[data-save="modal"]').click(function (event) {


            event.preventDefault();
            var form = $(this).parents('.modal').find('form');
            form.validate();
            var nesto = form.valid();
            var actionUrl = form.attr("action");
            var url = $(this).attr('data-url');
            var modalID = $(".modal").attr('id');
            var ajaxID = $("div[data-table=ajaxDiv]").attr('id');




            var dataToSend = new FormData(form.get(0));

            if (nesto) {
                $.ajax({ url: actionUrl,async:false, method: 'post', data: dataToSend, processData: false, contentType: false }).
                    done(function (data) {
                        var newBody = $('.modal-body', data);
                        $("#" + modalID).find('.modal-body').replaceWith(newBody);



                        if (newBody.find('[name="IsValid"]').val() !== "False") {

                            $('.modal').modal('hide');

                            $.ajax({
                                url: url, success: function (result) {
                                    $("#" + ajaxID).html(result);
                                }
                            });
                        }

                    });
            }




        });

 });
