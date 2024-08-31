document.addEventListener('DOMContentLoaded', function() {
    const username = localStorage.getItem('username');
    document.getElementById('username').textContent = username || 'Guest';
});

function showSection(sectionId) {
    const sections = document.querySelectorAll('.content-section');
    sections.forEach(section => {
        section.style.display = 'none'; // Hide all sections
    });

    const activeSection = document.getElementById(`${sectionId}-section`);
    if (activeSection) {
        activeSection.style.display = 'block'; // Show the selected section
    }

    // Optionally, load data if the section is shown for the first time or needs to refresh
    if (sectionId === 'orders') {
        loadOrders();
    } else if (sectionId === 'details') {
        loadUserDetails();
    }
}

function loadOrders() {
    // Placeholder for order loading function
}

function loadUserDetails() {
    const username = localStorage.getItem('username');

    if (!username) {
        document.getElementById('details-container').innerHTML = '<p>No user details available. Please log in.</p>';
        return;
    }

    fetch(`http://localhost:5163/api/Users/GetUserByUsername/${username}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    })
    .then(response => {
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return response.json();
    })
    .then(data => {
        console.log("API response:", data);  // Check the structure here

        if (data) {
            // Use the exact keys from the API response
            const userDetailsHtml = `
                <p><strong>User ID:</strong> ${data.kullanıcıId || ''}</p>
                <p><strong>Username:</strong> ${data.kullanıcıAdı || ''}</p>
                <p><strong>Email:</strong> ${data.email || ''}</p>
            `;
            document.getElementById('details-container').innerHTML = userDetailsHtml;
        } else {
            document.getElementById('details-container').innerHTML = '<p>User data not found.</p>';
        }
    })
    .catch(error => {
        console.error('Failed to fetch user details:', error);
        document.getElementById('details-container').innerHTML = `<p>Error loading user details. Please try again later.</p>`;
    });
}