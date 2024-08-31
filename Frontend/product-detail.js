document.addEventListener('DOMContentLoaded', function() {
    const queryParams = new URLSearchParams(window.location.search);
    const productId = queryParams.get('ürünId'); 

    if (productId) {
        fetch(`http://localhost:5163/api/Products/${productId}`)
        .then(response => response.json())
        .then(product => {
            document.getElementById('product-name').textContent = product.İsim;
            document.getElementById('product-description').textContent = product.açıklama;
            document.getElementById('product-price').textContent = product.fiyat.toFixed(2);
            document.getElementById('product-stock').textContent = product.stok;
            document.getElementById('product-image').src = `/Users/akk/Desktop/Code/e-commerce_app/Frontend/Assets/product ${productId}.webp`;
            document.getElementById('product-image').alt = product.İsim;

           
            document.getElementById('add-to-cart-button').onclick = function() {
                addToCart(product); 
            };
        })
        .catch(error => {
            console.error('Failed to fetch product details:', error);
            const container = document.getElementById('productDetailContainer');
            container.innerHTML = `<p>Error loading product details.</p>`;
        });
    }
});
function addToCart(product) {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];
    const existingItemIndex = cart.findIndex(item => item.productId === product.ÜrünId);

    if (existingItemIndex !== -1) {
        cart[existingItemIndex].quantity += 1;
    } else {
        cart.push({
            productId: product.ÜrünId,
            productName: product.İsim,
            price: product.Fiyat,
            quantity: 1
        });
    }

    localStorage.setItem('cart', JSON.stringify(cart));
    alert(`${product.İsim} has been added to your cart.`);
}