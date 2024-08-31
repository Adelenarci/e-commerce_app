document.addEventListener('DOMContentLoaded', function() {
    document.getElementById('addProductForm').addEventListener('submit', function(event) {
        event.preventDefault();
    
        // Son ürünü getir ve yeni ürün için ID hesapla
        fetch('http://localhost:5163/api/Admin/products/last')
        .then(async response => {
            if (!response.ok) {
                // Başarısız istek durumunda hata işleme
                const error = await response.json();
                return await Promise.reject('Failed to fetch last product: ' + (error.message || JSON.stringify(error)));
            }
            return response.json();
        })
        .then(data => {
            const newId = data.ürünId + 1;  // Son ürün ID'sini al ve bir artır
            const productPayload = {
                product: { 
                    ÜrünId: newId,
                    İsim: document.getElementById('productName').value.trim(),
                    Açıklama: document.getElementById('productDescription').value.trim(),
                    Fiyat: parseFloat(document.getElementById('productPrice').value),
                    Stok: parseInt(document.getElementById('productStock').value),
                }
            };
        
            console.log("Sending data to server:", productPayload);
        
            // Yeni ürünü sunucuya gönder
            return fetch('http://localhost:5163/api/Admin/products', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(productPayload)
            });
        })
        .then(async response => {
            if (!response.ok) {
                // POST isteği başarısız olduysa hata işleme
                const error = await response.json();
                return await Promise.reject('Failed to add product: ' + (error.message || JSON.stringify(error)));
            }
            return response.json();
        })
        .then(data => {
            alert('Product added successfully');
            console.log("Product added:", data);
            document.getElementById('addProductForm').reset(); // Başarılı kayıttan sonra formu sıfırla
        })
        .catch(error => {
            console.error("Error:", error);
            alert(error); // Hata mesajını göster
        });
    });
    document.getElementById('updateProductForm').addEventListener('submit', function(event) {
        event.preventDefault();
        const id = parseInt(document.getElementById('updateId').value);
        const product = {
            ÜrünId: id,
            İsim: document.getElementById('updateName').value.trim(),
            Açıklama: document.getElementById('updateDescription').value.trim(),
            Fiyat: parseFloat(document.getElementById('updatePrice').value),
            Stok: parseInt(document.getElementById('updateStock').value),
        };

        fetch(`http://localhost:5163/api/Admin/products/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(product)
        })
        .then(response => {
            if (response.ok) {
                alert('Product updated successfully');
                document.getElementById('updateProductForm').reset(); // Reset form after successful update
            } else {
                throw new Error('Failed to update product');
            }
        })
        .catch(error => {
            console.error('Error updating product:', error);
            alert(error.message);
        });
    });
});