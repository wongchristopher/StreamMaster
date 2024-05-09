import { type DataTableExpandedRows, type DataTableFilterMeta, type DataTableValue } from 'primereact/datatable';
import { useLocalStorage } from 'primereact/hooks';
import { useState } from 'react';

import { AdditionalFilterProperties } from '@lib/common/common';

import { useSelectAll } from '@lib/redux/hooks/selectAll';
import { useSelectedItems } from '@lib/redux/hooks/selectedItems';
import { useShowHidden } from '@lib/redux/hooks/showHidden';
import { useShowSelections } from '@lib/redux/hooks/showSelections';
import { useSortInfo } from '@lib/redux/hooks/sortInfo';
import useEntityStorage from '@lib/redux/hooks/useEntityStorage';
import createEntitySlice from '@lib/redux/slices/entitySlice';
import { ColumnMeta, PagedTableInformation } from './DataSelectorTypes';

const useSMDataSelectorState = <T extends DataTableValue>(id: string, selectedItemsKey: string) => {
  const { sortInfo, setSortInfo } = useSortInfo(id);
  const { selectAll, setSelectAll } = useSelectAll(id);
  const { selectedItems, setSelectedItems } = useSelectedItems<T>(selectedItemsKey);
  const { showHidden } = useShowHidden(id);
  const { showSelections, setShowSelections } = useShowSelections(id);

  const [rowClick, setRowClick] = useLocalStorage<boolean>(false, `${id}-rowClick`);
  const [visibleColumns, setVisibleColumns] = useLocalStorage<ColumnMeta[] | undefined | null>(null, `${id}-visibleColumns`);

  const [pagedInformation, setPagedInformation] = useState<PagedTableInformation>();
  const [previousDataSource, setPreviousDataSource] = useState<T[] | undefined>();
  const [dataSource, setDataSource] = useState<T[]>();
  const [first, setFirst] = useState<number>(0);
  const [page, setPage] = useState<number>(1);
  const [additionalFilterProperties, setAdditionalFilterProperties] = useState<AdditionalFilterProperties | undefined>();

  const [rows, setRows] = useState<number>(25);
  const [filters, setFilters] = useState<DataTableFilterMeta>({});
  const [expandedRows, setExpandedRows] = useState<DataTableExpandedRows>();

  const entitySlice = createEntitySlice<T>();
  const { setEntityValue, removeEntityValue } = useEntityStorage(id, entitySlice);

  const setSortField = (value: string) => {
    setSortInfo({ sortField: value });
  };

  const setSortOrder = (value: -1 | 0 | 1) => {
    setSortInfo({ sortOrder: value });
  };

  const sortField = sortInfo?.sortField ?? '';
  const sortOrder = sortInfo?.sortOrder ?? 1;

  return {
    setters: {
      setAdditionalFilterProps: setAdditionalFilterProperties,
      setDataSource,
      setExpandedRows,
      setFilters,
      setFirst,
      setPage,
      setPagedInformation,
      setPrevDataSource: setPreviousDataSource,
      setRowClick,
      setRows,
      setSelectAll,
      setSelectedItems,
      setShowSelections,
      setSortField,
      setSortOrder,
      setEntityValue,
      removeEntityValue,
      setVisibleColumns
    },
    state: {
      additionalFilterProps: additionalFilterProperties,
      dataSource,
      expandedRows,
      filters,
      first,
      page,
      pagedInformation,
      prevDataSource: previousDataSource,
      rowClick,
      rows,
      selectAll,
      selectedItems,
      showHidden,
      showSelections,
      sortField,
      sortOrder,
      visibleColumns
    }
  };
};

export default useSMDataSelectorState;
