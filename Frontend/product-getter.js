document.addEventListener('DOMContentLoaded', function() {
    fetchProducts();
    setupLoginLogout();
});

function fetchProducts() {
    fetch('http://localhost:5163/api/Products')
    .then(response => response.json())
    .then(products => {
        const productList = document.getElementById('productList');
        productList.innerHTML = ''; // Clear existing products before loading new ones
        products.forEach(product => {
            const productElement = document.createElement('div');
            productElement.className = 'product-item';
            productElement.innerHTML = `
                <img src="/Users/akk/Desktop/Code/e-commerce_app/Frontend/Assets/product ${product.ürünId}.webp" alt="${product.İsim}">
                <h3>${product.İsim}</h3>
                <p>Price: $${product.fiyat.toFixed(2)}</p>
                <p>Stock: ${product.stok}</p>
                <button onclick="navigateToProductDetail(${product.ürünId})" class="btn">Details</button>
            `;
            productList.appendChild(productElement);
        });
    })
    .catch(error => {
        console.error('Failed to fetch products:', error); // Log fetch errors
    });
}
function navigateToProductDetail(ürünId) {
    window.location.href = `product-detail.html?ürünId=${ürünId}`;
}