﻿

function OpenPdfFile(fileName, byteBase64) {

	var link = document.createElement('a');
	link.download = fileName;
	link.href = "data:application/pdf;base64," + byteBase64;
	document.body.appendChild(link);
	link.click();
	document.body.removeChild(link);
}


