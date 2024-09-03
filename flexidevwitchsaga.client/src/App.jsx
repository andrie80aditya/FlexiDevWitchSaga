import React, { useState } from 'react';
import axios from 'axios';
import './App.css'; // We'll create this file next

function App() {
    const [personA, setPersonA] = useState({ ageOfDeath: '', yearOfDeath: '' });
    const [personB, setPersonB] = useState({ ageOfDeath: '', yearOfDeath: '' });
    const [result, setResult] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        setError(null);
        try {
            const response = await axios.post('/api/witch/calculate', [personA, personB]);
            setResult(response.data);
        } catch (error) {
            console.error('Error:', error);
            setError('An error occurred while calculating. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    return (
        <div className="app-container">
            <header>
                <h1>FlexiDev Witch Saga</h1>
                <p>Calculate the average number of people killed by witches</p>
            </header>
            <main>
                <form onSubmit={handleSubmit}>
                    <div className="input-group">
                        <h2>Person A</h2>
                        <input
                            type="number"
                            placeholder="Age of Death"
                            value={personA.ageOfDeath}
                            onChange={(e) => setPersonA({ ...personA, ageOfDeath: e.target.value })}
                            required
                        />
                        <input
                            type="number"
                            placeholder="Year of Death"
                            value={personA.yearOfDeath}
                            onChange={(e) => setPersonA({ ...personA, yearOfDeath: e.target.value })}
                            required
                        />
                    </div>
                    <div className="input-group">
                        <h2>Person B</h2>
                        <input
                            type="number"
                            placeholder="Age of Death"
                            value={personB.ageOfDeath}
                            onChange={(e) => setPersonB({ ...personB, ageOfDeath: e.target.value })}
                            required
                        />
                        <input
                            type="number"
                            placeholder="Year of Death"
                            value={personB.yearOfDeath}
                            onChange={(e) => setPersonB({ ...personB, yearOfDeath: e.target.value })}
                            required
                        />
                    </div>
                    <button type="submit" disabled={loading}>
                        {loading ? 'Calculating...' : 'Calculate'}
                    </button>
                </form>
                {error && <div className="error">{error}</div>}
                {result && (
                    <div className="result">
                        <h2>Result</h2>
                        {result.isValid ? (
                            <p>Average number of people killed by witches: <span>{result.averageKilled.toFixed(2)}</span></p>
                        ) : (
                            <p>Invalid input. Please check your data and try again.</p>
                        )}
                    </div>
                )}
            </main>
            <footer>
                <p>&copy; 2024 FlexiDev Witch Saga. All rights reserved.</p>
            </footer>
        </div>
    );
}

export default App;