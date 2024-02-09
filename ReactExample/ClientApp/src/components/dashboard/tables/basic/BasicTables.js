// import node module libraries
import { Fragment } from 'react'
import { Breadcrumb, Col, Row } from 'react-bootstrap'

// import required sub component
import TableBasic from './TableBasic'
import TableDark from './TableDark'
import TableHeadOptions from './TableHeadOptions'
import TableStriped from './TableStriped'
import TableBordered from './TableBordered'
import TableBorderedColor from './TableBorderedColor'
import TableBorderless from './TableBorderless'
import TableHover from './TableHover'
import TableNesting from './TableNesting'
import TableActive from './TableActive'
import TableSmall from './TableSmall'

// import required data file
import BasicTableData from 'data/dashboard/tables/BasicTableData'
import BasicNestedTableData from 'data/dashboard/tables/BasicNestedTableData';

const BasicTables = () => {
  return (
    <Fragment>
      <Row>
        <Col lg={12} md={12} sm={12}>
          <div className="border-bottom pb-3 mb-3">
            <div className="mb-2 mb-lg-0">
              <h1 className="mb-1 h2 fw-bold">Tables </h1>
              <Breadcrumb>
                <Breadcrumb.Item href="#">Dashboard</Breadcrumb.Item>
                <Breadcrumb.Item href="#">Tables</Breadcrumb.Item>
                <Breadcrumb.Item active>Basic </Breadcrumb.Item>
              </Breadcrumb>
            </div>
          </div>
        </Col>
      </Row>
      <Row>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Basic */}
          <TableBasic TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Dark */}
          <TableDark TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/*  Table Head Options */}
          <TableHeadOptions TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Striped */}
          <TableStriped TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Bordered */}
          <TableBordered TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Bordered Color */}
          <TableBorderedColor TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Borderless */}
          <TableBorderless TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Hover */}
          <TableHover TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Nesting Table */}
          <TableNesting TableData={BasicNestedTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Active */}
          <TableActive TableData={BasicTableData} />
        </Col>

        <Col xl={6} xs={12} className="mb-5">
          {/* Table Small */}
          <TableSmall TableData={BasicTableData} />
        </Col>

      </Row>
    </Fragment>
  )
}

export default BasicTables