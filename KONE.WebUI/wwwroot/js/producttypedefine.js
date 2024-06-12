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

    // Genel yap�land�rma se�eneklerini belirleyin
    FilePond.setOptions({
        allowImagePreview: true,
        imagePreviewHeight: 200,
        allowFileTypeValidation: true,
        acceptedFileTypes: ['image/*']
    });

    // T�m dosya input ��elerini FilePond'a d�n��t�r�n
    FilePond.parse(document.body);

    const pickr = Pickr.create({
        el: '#color-picker', // Renk se�icinin eklenece�i elementin ID'si
        default: '#ff0000', // Varsay�lan renk
        theme: 'classic', // Tema
        swatches: [ // �nceden tan�ml� renk paleti
            '#ff0000', '#00ff00', '#0000ff', '#ffff00', '#ff00ff', '#00ffff'
        ],
        components: {
            // Renk bile�enleri
            preview: true,
            opacity: true,
            hue: true,
            interaction: {
                input: true,
                save: true
            }
        }
    });

    // Renk se�ici de�eri de�i�ti�inde
    pickr.on('change', (color) => {
        console.log(color.toHEXA().toString()); // Se�ilen rengi konsola yazd�r
    });
});