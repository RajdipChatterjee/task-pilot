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
} from "@fluentui/react-components";
import { EditRegular, DeleteRegular } from "@fluentui/react-icons";
import type { Todo } from "../models/Todo";

interface TodoListProps {
  todos: Todo[];
  toggleStatus: (id: string) => Promise<void>;
  deleteTodo: (id: string) => Promise<void>;
}

const useClasses = makeStyles({
  container: {
    display: "flex",
    gap: "5px",
    fontSize: "25px",
  },
});

function TodoList({ todos, toggleStatus, deleteTodo }: TodoListProps) {
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
          <TableCellLayout>
            {item.isCompleted ? "Completed" : "Pending"}
          </TableCellLayout>
        );
      },
    }),
    createTableColumn<Todo>({
      columnId: "Created At",
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
            <EditRegular
              className={classes.container}
              onClick={() => handleToggleStatus(item.id)}
            />
            <DeleteRegular
              className={classes.container}
              onClick={() => handleDeleteClick(item.id)}
            />
          </div>
        );
      },
    }),
  ];

  async function handleDeleteClick(id: string) {
    await deleteTodo(id);
  }

  async function handleToggleStatus(id: string) {
    await toggleStatus(id);
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
