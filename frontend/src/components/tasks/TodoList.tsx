import {
  DataGrid,
  makeStyles,
  type TableColumnDefinition,
  TableCellLayout,
  createTableColumn,
  DataGridHeader,
  DataGridRow,
  DataGridHeaderCell,
  DataGridBody,
  DataGridCell,
  type TableColumnId,
  type DataGridCellFocusMode,
  Badge,
  Button,
} from "@fluentui/react-components";
import { DeleteRegular } from "@fluentui/react-icons";
import type { Todo } from "../../models/Todo";
import { TodoStatus } from "../../enums/TodoStatus";
import TaskDialog from "./TaskDialog";
import { DialogMode } from "../../enums/DialogMode";

interface TodoListProps {
  todos: Todo[];
  deleteTodo: (id: string) => Promise<void>;
  loadTodos: () => Promise<void>;
}

const useClasses = makeStyles({
  container: {
    fontSize: "20px",
  },
});

function getBadgeProps(status: TodoStatus) {
  switch (status) {
    case TodoStatus.Done:
      return { color: "success" as const, label: "Done" };
    case TodoStatus.Completed:
      return { color: "brand" as const, label: "Completed" };
    case TodoStatus.Pending:
    default:
      return { color: "warning" as const, label: "Pending" };
  }
}

function TodoList({ todos, deleteTodo, loadTodos }: TodoListProps) {
  const classes = useClasses();

  const columns: TableColumnDefinition<Todo>[] = [
    createTableColumn<Todo>({
      columnId: "Task",
      compare: (a, b) => a.title.localeCompare(b.title),
      renderHeaderCell: () => "Task",
      renderCell: (item) => <TableCellLayout>{item.title}</TableCellLayout>,
    }),
    createTableColumn<Todo>({
      columnId: "Description",
      compare: (a, b) => (a.description || "").localeCompare(b.description || ""),
      renderHeaderCell: () => "Description",
      renderCell: (item) => <TableCellLayout>{item.description}</TableCellLayout>,
    }),
    createTableColumn<Todo>({
      columnId: "Status",
      compare: (a, b) => (a.status || "").localeCompare(b.status || ""),
      renderHeaderCell: () => "Status",
      renderCell: (item) => {
        const badgeProps = getBadgeProps(item.status);
        return (
          <Badge
            appearance="tint"
            size="extra-large"
            color={badgeProps.color}
          >
            {badgeProps.label}
          </Badge>
        );
      },
    }),
    createTableColumn<Todo>({
      columnId: "Actions",
      renderHeaderCell: () => "Actions",
      renderCell: (item) => {
        return (
          <div style={{ display: "flex", gap: "8px" }}>
            <TaskDialog mode={DialogMode.Edit} todo={item} onSuccess={() => loadTodos()} />
            <Button onClick={() => handleDeleteClick(item.id)}>
              <DeleteRegular className={classes.container} />
            </Button>
          </div>
        );
      },
    }),
  ];

  async function handleDeleteClick(id: string) {
    await deleteTodo(id);
  }

  const getCellFocusMode = (columnId: TableColumnId): DataGridCellFocusMode => {
    switch (columnId) {
      case "singleAction":
        return "none";
      case "actions":
        return "group";
      default:
        return "cell";
    }
  };

  return (
    <DataGrid
      items={todos}
      columns={columns}
      selectionMode="multiselect"
      subtleSelection
      getRowId={(item) => item.id}
      focusMode="composite"
      style={{ minWidth: "550px" }}
    >
      <DataGridHeader>
        <DataGridRow
          selectionCell={{
            checkboxIndicator: { "aria-label": "Select all rows" },
          }}
        >
          {({ renderHeaderCell }) => (
            <DataGridHeaderCell>{renderHeaderCell()}</DataGridHeaderCell>
          )}
        </DataGridRow>
      </DataGridHeader>
      <DataGridBody<Todo>>
        {({ item, rowId }) => (
          <DataGridRow<Todo>
            key={rowId}
            selectionCell={{
              checkboxIndicator: { "aria-label": "Select row" },
            }}
          >
            {({ renderCell, columnId }) => (
              <DataGridCell focusMode={getCellFocusMode(columnId)}>
                {renderCell(item)}
              </DataGridCell>
            )}
          </DataGridRow>
        )}
      </DataGridBody>
    </DataGrid>
  );
}

export default TodoList;
