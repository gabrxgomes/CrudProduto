function searchProducts() {
    const query = document.getElementById('searchInput').value;
    fetch(`/Products/search?name=${encodeURIComponent(query)}`)
        .then(response => response.json())
        .then(data => {
            const productList = document.getElementById('productList');
            productList.innerHTML = '';
            data.forEach(product => {
                const listItem = document.createElement('li');
                listItem.textContent = `${product.name} - $${product.price} - ${product.inStock ? 'In Stock' : 'Out of Stock'}`;
                productList.appendChild(listItem);
            });
        })
        .catch(error => console.error('Error fetching products:', error));
}
