document.addEventListener('DOMContentLoaded', function () {
    const chartElements = document.querySelectorAll('.bandwidth-chart');
    chartElements.forEach(element => {
        const ctx = element.getContext('2d');
        const chartType = element.dataset.chartType || 'bar';
        const data = JSON.parse(element.dataset.chartData);

        new Chart(ctx, {
            type: chartType,
            data: data,
            options: {
                responsive: true,
                plugins: {
                    legend: { position: 'top' },
                    tooltip: {
                        callbacks: {
                            label: context => `${context.dataset.label}: ${context.raw} Кбит/с`
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: 'Пропускная способность (Кбит/с)' }
                    }
                }
            }
        });
    });
});