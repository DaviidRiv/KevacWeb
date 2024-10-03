$(document).ready(function () {
    $('#dataTable').DataTable({
        language: {
            processing: "Procesando...",
            search: "Buscar:",
            lengthMenu: "Mostrar _MENU_ registros",
            info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
            infoEmpty: "Mostrando 0 a 0 de 0 registros",
            infoFiltered: "(filtrado de _MAX_ registros totales)",
            loadingRecords: "Cargando...",
            zeroRecords: "No se encontraron registros coincidentes",
            emptyTable: "No hay datos disponibles en la tabla",
            paginate: {
                first: "Primero",
                previous: "Anterior",
                next: "Siguiente",
                last: "Último"
            },
            aria: {
                sortAscending: ": activar para ordenar la columna ascendente",
                sortDescending: ": activar para ordenar la columna descendente"
            }
        },
        dom: '<"top"lfB>rt<"bottom"ip><"clear">', // Ajusta el layout para incluir lengthMenu y search
        buttons: [
            {
                extend: 'copy',
                text: 'Copiar',
                exportOptions: {
                    columns: ':not(:last-child)'
                },
                className: 'btn-exportar-copy me-1'
            },
            {
                extend: 'csv',
                text: 'CSV',
                exportOptions: {
                    columns: ':not(:last-child)'
                },
                className: 'btn-exportar-csv me-1'
            },
            {
                extend: 'excel',
                text: 'Excel',
                exportOptions: {
                    columns: ':not(:last-child)'
                },
                className: 'btn-exportar-excel me-1'
            },
            {
                extend: 'pdf',
                text: 'PDF',
                exportOptions: {
                    columns: ':not(:last-child)'
                },
                className: 'btn-exportar-pdf me-1'
            },
            {
                extend: 'print',
                text: 'Imprimir',
                exportOptions: {
                    columns: ':not(:last-child)'
                },
                className: 'btn-exportar-print me-1'
            },
            {
                extend: 'colvis',
                text: 'Columnas',
                className: 'btn-colvis'
            }
        ]
    });
});

