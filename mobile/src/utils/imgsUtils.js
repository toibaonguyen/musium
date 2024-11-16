const getImageBase64 = async (imageUrl) => {
  try {
    const response = await fetch(imageUrl);
    const blob = await response.blob();
    const reader = new FileReader();
    reader.readAsDataURL(blob);
    return new Promise((resolve, reject) => {
      reader.onloadend = () => {
        const base64data = reader.result.split(',')[1];
        resolve(base64data);
      };
      reader.onerror = reject;
    });
  } catch (error) {
    console.error('Error fetching image:', error);
    return null;
  }
};

const getImageBase64Array = async (imageUrls) => {
  const base64Array = [];
  for (const imageUrl of imageUrls) {
    const base64data = await getImageBase64(imageUrl);
    if (base64data) {
      const base64String = `data:image/jpeg;base64,${base64data}`;
      base64Array.push(base64String);
    }
  }
  return base64Array;
};

export { getImageBase64, getImageBase64Array };
