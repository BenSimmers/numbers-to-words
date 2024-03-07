/**
 */
export default function LazyNumberDisplay(props: any): JSX.Element {
  return (
    <div className="mt-4">
      <p>{props.data}</p>
    </div>
  );
}