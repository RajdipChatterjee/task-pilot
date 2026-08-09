import {
  SearchBox,
  Toolbar,
} from "@fluentui/react-components";

import TaskDialog from "./TaskDialog";
import {DialogMode} from "../enums/DialogMode";

type ToolBarProps = {
  loadTodos: () => Promise<void>;
}

function ToolBar({loadTodos} : ToolBarProps) {
  return (
    <Toolbar style={{ display: "flex", justifyContent: "space-between" }}>
      <SearchBox placeholder="Search tasks..." />
      <TaskDialog mode={DialogMode.Create} onSuccess={() => loadTodos()}/>
    </Toolbar>
  );
}

export default ToolBar;
