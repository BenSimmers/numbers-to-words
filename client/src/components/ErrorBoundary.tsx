/**
 * Error boundary component
 * @param error - The error to display
 * @returns
 */
const ErrorBoundary = ({ error }: { error: Error }): JSX.Element => {
  return <div>{error.message}</div>;
};

export default ErrorBoundary;
