import {
  Card,
  CardHeader,
  Text,
  Button
} from "@fluentui/react-components"

import type { Todo } from "../models/Todo";

interface TodoCardProps {
  todo: Todo,
  handleToggleStatus: (id: string) => Promise<void>,
  handleDeleteClick: (id: string) => Promise<void>
}

function TodoCard({todo, handleToggleStatus, handleDeleteClick}: TodoCardProps) {

  return (
    <Card key={todo.id} style={{ marginTop: 16, padding: 16 }}>
      <CardHeader
        header={<Text weight="semibold">{todo.title}</Text>}
        description={
          <div>
            <Text>{todo.description}</Text>

            <br />

            <Text size={200}>
              {todo.isCompleted ? "✅ Completed" : "⏳ Pending"}
            </Text>
          </div>
        }
      />
      <div
        style={{
          marginTop: 16,
          display: "flex",
          justifyContent: "space-between",
        }}
      >
        <Button
          appearance="primary"
          onClick={async () => handleToggleStatus(todo.id)}
        >
          {todo.isCompleted ? "Mark Pending" : "Mark Complete"}
        </Button>

        <Button
          appearance="secondary"
          onClick={async () => handleDeleteClick(todo.id)}
        >
          Delete
        </Button>
      </div>
    </Card>
  );
}

export default TodoCard;
