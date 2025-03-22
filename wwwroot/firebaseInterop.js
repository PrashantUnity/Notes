let db;

function initializeFirebase(config) {
    const app = firebase.initializeApp(config);
    db = firebase.firestore();
    return true;
}

async function saveContact(formData) {
    try {
        await db.collection('contacts').add({
            ...formData,
            timestamp: firebase.firestore.FieldValue.serverTimestamp()
        });
        return { success: true };
    } catch (error) {
        return { success: false, error: error.message };
    }
}

// In firebaseInterop.js
async function getContacts() {
    try {
        const snapshot = await db.collection('contacts')
            .orderBy('timestamp', 'desc')
            .get();

        const contacts = [];
        snapshot.forEach(doc => {
            const data = doc.data();
            contacts.push({
                id: doc.id,
                name: data.name || '',
                email: data.email || '',
                description: data.description || '',
                timestamp: data.timestamp?.toMillis() || Date.now()
            });
        });
        return contacts;
    } catch (error) {
        console.error('Error getting contacts:', error);
        return [];
    }
}

async function deleteContact(contactId) {
    try {
        await db.collection('contacts').doc(contactId).delete();
        return { success: true };
    } catch (error) {
        return { success: false, error: error.message };
    }
}
async function updateContact(contactId, updatedData) {
    try {
        await db.collection('contacts').doc(contactId).update(updatedData);
        return { success: true };
    } catch (error) {
        return { success: false, error: error.message };
    }
}

async function getContactsByPage(pageNumber = 1, pageSize = 10) {
    try {
        // Calculate how many documents to skip
        const offset = (pageNumber - 1) * pageSize;

        // Firestore query with limit and offset simulation
        const snapshot = await db.collection('contacts')
            .orderBy('timestamp', 'desc')
            .limit(offset + pageSize)
            .get();

        if (!snapshot.empty) {
            const docs = snapshot.docs.slice(offset); // Get only the current page's items

            // Map the docs into usable data
            const contacts = docs.map(doc => ({
                id: doc.id,
                ...doc.data(),
                timestamp: doc.data().timestamp?.toMillis() || Date.now()
            }));

            return contacts;
        } else {
            return [];
        }
    } catch (error) {
        console.error('Error getting contacts:', error);
        return [];
    }
}