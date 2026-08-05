import { Card, CardHeader, Text, Field, Button } from "@fluentui/react-components";


async function TodoCard() {
    async function handleAddTodo() {
        const todo: CreateTodo = {
          title,
          description,
        };
    
        await todoApi.createTodo(todo);
    
        setTitle("");
        setDescription("");
    
        await loadTodos();
      }
      
  return (
    <Card>
      <CardHeader
        header={
          <Text size={700} weight="semibold">
            Todo Application
          </Text>
        }
        description="Built with React + ASP.NET Core"
      />

      <div
        style={{
          display: "flex",
          flexDirection: "column",
          gap: 16,
          marginTop: 20,
        }}
      >
        <Field
          label="Title"
          validationState={title ? "none" : "warning"}
          validationMessage={title ? "" : "Title is required"}
        >
          <Input value={title} onChange={(_, data) => setTitle(data.value)} />
        </Field>

        <Field label="Description">
          <Textarea
            value={description}
            onChange={(_, data) => setDescription(data.value)}
          />
        </Field>

        {/* <Button appearance="primary">Add Todo</Button> */}
      </div>
      <div style={{ marginTop: 20 }}>
        <Button appearance="primary" onClick={handleAddTodo}>
          Add Todo
        </Button>
      </div>
    </Card>
  );
}


export default TodoCard;