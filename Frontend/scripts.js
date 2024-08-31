function addToCart(product) {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    
    const cartItem = {
        productId: product.ÜrünId,  
        productName: product.İsim,   
        price: product.Fiyat,        
        quantity: 1                  
    };

    
    const existingItemIndex = cart.findIndex(item => item.productId === cartItem.productId);
    if (existingItemIndex !== -1) {
       
        cart[existingItemIndex].quantity += 1;
    } else {
        
        cart.push(cartItem);
    }

    
    localStorage.setItem('cart', JSON.stringify(cart));
    alert(`${cartItem.productName} has been added to your cart.`);
}