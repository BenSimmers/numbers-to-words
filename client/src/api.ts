
/**
 * Fetch data from the server and return it as a promise
 * @param url - The URL to fetch data from
 * @param options - The options to pass to the fetch function
 * @returns A promise that resolves to the fetched data
 */
export async function fetchData<T>(url: string, options?: RequestInit) {
  const response = await fetch(url, options);
  if (!response.ok) {
    throw new Error("Network response was not ok");
  }
  const data = await response.json();
  return data as T;
}
