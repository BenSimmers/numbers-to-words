import React, { useState } from "react";
import { fetchData } from "../api";

/**
 * A form that allows the user to enter a number and convert it to words
 * @returns A form that allows the user to enter a number and convert
 * it to words
 */
const NumberToWordsForm = () => {
  const [data, setData] = useState<string | null>(null);
  const [number, setNumber] = useState<string>("");
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);
    setError("");
    try {
      const result = await fetchData(
        `http://localhost:5000/api/NumbersToWords/${number}`
      );
      setData(result as string);
    } catch (error) {
      setError(new Error("Network response was not ok").message);
    }
    setLoading(false);
  };

  return (
    <div className="container mx-auto p-4 bg-gray-100 text-center mt-4">
      <h1 className="text-2xl font-bold">Number to Words</h1>
      <form onSubmit={handleSubmit} className="bg-white p-4">
        <input
          type="text"
          placeholder="Enter a number"
          className="border p-2 w-1/2"
          value={number}
          onChange={(e) => setNumber(e.target.value)}
        />
        <button
          type="submit"
          className="ml-4 bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
        >
          Convert
        </button>

        {loading && <div>Loading...</div>}
        {error && <div>{error}</div>}
        {data && <div>{(data as unknown as { words: string }).words}</div>}
      </form>
    </div>
  );
};

export default NumberToWordsForm;
