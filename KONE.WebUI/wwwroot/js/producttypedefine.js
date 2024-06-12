$(document).ready(function () {
    // Eklentileri kaydedin
    FilePond.registerPlugin(
        FilePondPluginFileEncode,
        FilePondPluginFileValidateSize,
        FilePondPluginFileValidateType,
        FilePondPluginImageCrop,
        FilePondPluginImageEdit,
        FilePondPluginImageExifOrientation,
        FilePondPluginImagePreview,
        FilePondPluginImageResize,
        FilePondPluginImageTransform
    );

    // Genel yapýlandýrma seçeneklerini belirleyin
    FilePond.setOptions({
        allowImagePreview: true,
        imagePreviewHeight: 200,
        allowFileTypeValidation: true,
        acceptedFileTypes: ['image/*']
    });

    // Tüm dosya input öðelerini FilePond'a dönüþtürün
    FilePond.parse(document.body);

    const pickr = Pickr.create({
        el: '#color-picker', // Renk seçicinin ekleneceði elementin ID'si
        default: '#ff0000', // Varsayýlan renk
        theme: 'classic', // Tema
        swatches: [ // Önceden tanýmlý renk paleti
            '#ff0000', '#00ff00', '#0000ff', '#ffff00', '#ff00ff', '#00ffff'
        ],
        components: {
            // Renk bileþenleri
            preview: true,
            opacity: true,
            hue: true,
            interaction: {
                input: true,
                save: true
            }
        }
    });

    // Renk seçici deðeri deðiþtiðinde
    pickr.on('change', (color) => {
        console.log(color.toHEXA().toString()); // Seçilen rengi konsola yazdýr
    });
});