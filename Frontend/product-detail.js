document.addEventListener('DOMContentLoaded', function() {
    const queryParams = new URLSearchParams(window.location.search);
    const productId = queryParams.get('id'); // Assume the URL is like ?id=123

    if (productId) {
        fetch(`http://localhost:5163/api/Products/${productId}`)
        .then(response => response.json())
        .then(product => {
            const container = document.getElementById('productDetailContainer');
            container.innerHTML = `
                <h2>${product.İsim}</h2>
                <p>${product.Açıklama}</p>
                <p>Price: $${product.Fiyat.toFixed(2)}</p>
                <p>Stock: ${product.Stok}</p>
            `;
        })
        .catch(error => {
            console.error('Failed to fetch product details:', error);
            container.innerHTML = `<p>Error loading product details.</p>`;
        });
    }
});