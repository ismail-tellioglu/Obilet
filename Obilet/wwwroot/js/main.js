const showModal = (modalTitle, message) => {
    $(".modal-title").html(modalTitle);
    $("#modal-content").html(message);
    $('#myModal').modal("show");

    setTimeout(function () {
        $('#myModal').modal("hide");
    }, 3000);
};
$('#swap-img').on('click', function () {
    let origin = $('#originInput').find(":selected").val();
    let dest = $('#destinationInput').find(":selected").val();
    $('#originInput option[value="' + dest + '"]').prop('selected', true);
    $('#destinationInput option[value="' + origin + '"]').prop('selected', true);
});
$('#btn-set-today').on('click', function () {
    let date = new Date();
    let today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#DepartureTime').datepicker('setDate', today);

});

$('#originInput').change(function () {

    let oldValue = $(this).data("prev");
    let newValue = $('#originInput').find(":selected").val();
    let dest = $('#destinationInput').find(":selected").val();
    if (newValue === dest) {
        //alert("Origin and destination can not be same location");
        showModal("Warning!!", "Origin and destination can not be same");
        $('#originInput option[value="' + oldValue + '"]').prop('selected', true);
    }
    else {
        $(this).data('prev', newValue);
    }
});

$('#destinationInput').change(function () {

    let oldValue = $(this).data("prev");
    console.log(oldValue);
    let newValue = $('#destinationInput').find(":selected").val();
    let origin = $('#originInput').find(":selected").val();
    if (newValue === origin) {
        //alert("Origin and destination can not be same location");
        showModal("Warning!!", "Origin and destination can not be same");
        $('#destinationInput option[value="' + oldValue + '"]').prop('selected', true);
    }
    else {
        $(this).data('prev', newValue);
    }
});

$('#btn-set-tomorrow').on('click', function () {
    let date = new Date();
    date.setDate(date.getDate() + 1);
    let tomorrow = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    $('#DepartureTime').datepicker('setDate', tomorrow);

});