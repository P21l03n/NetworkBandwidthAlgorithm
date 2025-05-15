// Основная логика для динамического добавления устройств
document.addEventListener('DOMContentLoaded', function () {
    // Обработка выбора устройства из пресетов
    document.addEventListener('change', function (e) {
        if (e.target && e.target.matches('select[name$="].Name"]')) {
            const select = e.target;
            const preset = select.options[select.selectedIndex].text;
            const matches = preset.match(/\(([^,]+),\s*([^)]+)/);

            if (matches && matches.length >= 3) {
                const protocol = matches[1];
                const load = parseFloat(matches[2]);

                const deviceEntry = select.closest('.device-entry');
                deviceEntry.querySelector('select[name$="].Protocol"]').value = protocol;
                deviceEntry.querySelector('input[name$="].Load"]').value = load;
            }
        }
    });
});