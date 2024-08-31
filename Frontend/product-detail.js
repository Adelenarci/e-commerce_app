document.addEventListener('DOMContentLoaded', function() {
    const queryParams = new URLSearchParams(window.location.search);
    const productId = queryParams.get('ürünId'); // Assume the URL is like ?ürünId=123

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

            // Attach the addToCart function to the button with the correct product data
            document.getElementById('add-to-cart-button').onclick = function() {
                addToCart(product); // Pass the product object to addToCart
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

    // Create a cart item with all necessary properties
    const cartItem = {
        productId: product.ürünId,   // Use the product's ÜrünId
        productName: product.İsim,   // Use the product's İsim
        price: product.fiyat,        // Use the product's Fiyat
        quantity: 1                  // Default to 1, can be modified later if needed
    };

    // Check if the product is already in the cart
    const existingItemIndex = cart.findIndex(item => item.productId === cartItem.productId);
    if (existingItemIndex !== -1) {
        // If the product is already in the cart, increase the quantity
        cart[existingItemIndex].quantity += 1;
    } else {
        // If the product is not in the cart, add it
        cart.push(cartItem);
    }

    // Save the updated cart back to localStorage
    localStorage.setItem('cart', JSON.stringify(cart));
    alert(`${cartItem.productName} has been added to your cart.`);
}