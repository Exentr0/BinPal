// ImageService

import { Injectable } from '@angular/core';
import * as JSZip from 'jszip';

@Injectable({
    providedIn: 'root',
})
export class ImageService {
    constructor() {}

    async zipImages(images: File[]): Promise<Blob> {
        const zip = new JSZip();

        // Add each image to the zip file
        images.forEach((image, index) => {
            const fileName = `image_${index + 1}.png`;
            zip.file(fileName, image);
        });

        // Generate the zip file
        return await zip.generateAsync({ type: 'blob' });
    }

    // Make this method public
    public async convertImagesToZip(images: File[]): Promise<FormData> {
        const zipFile = await this.zipImages(images);
        const formData = new FormData();
        formData.append('zipFile', zipFile, 'images.zip');
        return formData;
    }
}
