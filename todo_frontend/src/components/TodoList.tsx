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
import type { Todo } from "../models/Todo";
import TaskDialog from "./TaskDialog";
import { DialogMode } from "../enums/DialogMode";

interface TodoListProps {
  todos: Todo[];
  toggleStatus: (id: string) => Promise<void>;
  deleteTodo: (id: string) => Promise<void>;
}

const useClasses = makeStyles({
  container: {
    fontSize: "20px",
  },
});

function TodoList({ todos, deleteTodo }: TodoListProps) {
  const classes = useClasses();

  const columns: TableColumnDefinition<Todo>[] = [
    createTableColumn<Todo>({
      columnId: "Task",
      compare: (a, b) => {
        return a.title.localeCompare(b.title);
      },
      renderHeaderCell: () => {
        return "Task";
      },
      renderCell: (item) => {
        return <TableCellLayout>{item.title}</TableCellLayout>;
      },
    }),
    createTableColumn<Todo>({
      columnId: "Description",
      compare: (a, b) => {
        return a.title.localeCompare(b.title);
      },
      renderHeaderCell: () => {
        return "Description";
      },
      renderCell: (item) => {
        return <TableCellLayout>{item.description}</TableCellLayout>;
      },
    }),
    createTableColumn<Todo>({
      columnId: "Status",
      compare: (a, b) => {
        return a.title.localeCompare(b.title);
      },
      renderHeaderCell: () => {
        return "Status";
      },
      renderCell: (item) => {
        return (
          <Badge
            appearance="tint"
            size="extra-large"
            color={item.isCompleted ? "brand" : "warning"}
          >
            {item.isCompleted ? "Completed" : "Pending"}
          </Badge>
        );
      },
    }),
    createTableColumn<Todo>({
      columnId: "Actions",
      compare: (a, b) => {
        return a.title.localeCompare(b.title);
      },
      renderHeaderCell: () => {
        return "Actions";
      },
      renderCell: (item) => {
        return (
          <div style={{ display: "flex", justifyContent: "space-between" }}>
            <TaskDialog mode={DialogMode.Edit} todo={item}/>
            <Button
              onClick={() => handleDeleteClick(item.id)}
            >
              <DeleteRegular
                className={classes.container}
              />
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
