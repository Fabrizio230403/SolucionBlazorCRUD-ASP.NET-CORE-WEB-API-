// Función para crear el gráfico de barras
window.crearGraficoBarras = (canvasId, chartData, options) => {
    var ctx = document.getElementById(canvasId);
    if (ctx) {
        new Chart(ctx, {
            type: 'bar',   
            data: chartData,
            options: options
        });
    } else {
        console.error(`No se encontró el canvas con id: ${canvasId}`);
    }
};

// Función para crear el gráfico circular 
function crearGraficoCircular(canvasId, chartData, options) {
    var ctx = document.getElementById(canvasId)?.getContext('2d');   
    if (ctx) {
        new Chart(ctx, {
            type: 'doughnut',  
            data: chartData,
            options: options
        });
    } else {
        console.error(`No se encontró el canvas con id: ${canvasId}`);
    }
}

function crearGraficoLinea(id, data, options) {
    const ctx = document.getElementById(id).getContext('2d');
    new Chart(ctx, {
        type: 'line',
        data: data,
        options: options
    });
}

//  DOM cargado antes de inicializar los tooltips
document.addEventListener('DOMContentLoaded', function () {
    // Inicializar todos los tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
});


//Funcion para filtrar tareas en la caja de busqueda
function filtrarTareas() {
    const input = document.getElementById('searchBox');
    const filter = input.value.toLowerCase();
    const tableBody = document.getElementById('tareasTableBody');
    const rows = tableBody.getElementsByTagName('tr');

    for (let i = 0; i < rows.length; i++) {
        let tareaNombre = rows[i].getElementsByTagName('td')[0].textContent || rows[i].getElementsByTagName('td')[0].innerText;
        if (tareaNombre.toLowerCase().indexOf(filter) > -1) {
            rows[i].style.display = '';
        } else {
            rows[i].style.display = 'none';
        }
    }
}