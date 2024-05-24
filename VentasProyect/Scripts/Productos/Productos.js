function showPopup(imageUrl) {
    document.getElementById('popupImage').src = imageUrl;
    document.getElementById('imagePopup').style.display = 'flex';
    document.body.classList.add('popup-open');
}

function hidePopup() {
    document.getElementById('imagePopup').style.display = 'none';
    document.body.classList.remove('popup-open');
}