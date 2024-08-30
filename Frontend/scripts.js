function addToCart(product) {
    const cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Create a cart item with all necessary properties
    const cartItem = {
        productId: product.ÜrünId,   // Use the product's ÜrünId
        productName: product.İsim,   // Use the product's İsim
        price: product.Fiyat,        // Use the product's Fiyat
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