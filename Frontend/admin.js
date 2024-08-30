document.getElementById('addProductForm').addEventListener('submit', function(event) {
    event.preventDefault();
    // First, fetch the last product to get the highest ID
    fetch('http://localhost:5163/api/Admin/products/last')
    .then(response => response.json())
    .then(data => {
        const newId = data.ÜrünId + 1;  // Increment the last product ID by one
        const product = {
            ÜrünId: newId,
            İsim: document.getElementById('productName').value,
            Açıklama: document.getElementById('productDescription').value,
            Fiyat: parseFloat(document.getElementById('productPrice').value),
            Stok: parseInt(document.getElementById('productStock').value),
        };

        // Now submit the new product with the incremented ID
        fetch('http://localhost:5163/api/Admin/products', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(product)
        }).then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Failed to add product');
            }
        }).then(data => {
            alert('Product added successfully');
            console.log(data);
        }).catch(error => {
            console.error('Error:', error);
        });
    })
    .catch(error => {
        console.error('Error fetching last product:', error);
    });
});

document.getElementById('updateProductForm').addEventListener('submit', function(event) {
    event.preventDefault();
    const id = document.getElementById('updateId').value;
    const product = {
        ÜrünId: id,
        İsim: document.getElementById('updateName').value,
        Açıklama: document.getElementById('updateDescription').value, // Added description field
        Fiyat: parseFloat(document.getElementById('updatePrice').value),
        Stok: parseInt(document.getElementById('updateStock').value),
    };

    fetch(`http://localhost:5163/api/Admin/products/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(product)
    }).then(response => {
        if (response.ok) {
            alert('Product updated successfully');
        } else {
            throw new Error('Failed to update product');
        }
    }).catch(error => {
        console.error('Error:', error);
    });
});