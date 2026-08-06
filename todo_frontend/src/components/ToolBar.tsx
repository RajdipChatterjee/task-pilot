import {
  SearchBox,
  Toolbar,
} from "@fluentui/react-components";

import TaskDialog from "./TaskDialog";
import {DialogMode} from "../enums/DialogMode";

function ToolBar() {
  return (
    <Toolbar style={{ display: "flex", justifyContent: "space-between" }}>
      <SearchBox placeholder="Search tasks..." />
      <TaskDialog mode={DialogMode.Create}/>
    </Toolbar>
  );
}

export default ToolBar;
